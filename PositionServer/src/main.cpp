#include "boost/asio.hpp"
#include "boost/bind.hpp"
#include "boost/date_time/posix_time/posix_time_types.hpp"
#include <array>
#include <functional>
#include <string>

#include "tracker.hpp"

namespace asio = boost::asio;

const short multicast_port = 2000;

class sender
{
public:
    sender(asio::io_service& io_service,
            const asio::ip::address& multicast_address)
        : endpoint(multicast_address, multicast_port),
        socket(io_service, endpoint.protocol())
    {
    }

    void send_position(std::array<float, 6> position) 
    {
        socket.async_send_to(asio::buffer(position), endpoint,
                [this](boost::system::error_code ec, std::size_t){
                    std::cout << "send message" << std::endl;
                });
    }

private:
    asio::ip::udp::endpoint endpoint;
    asio::ip::udp::socket socket;
};

std::array<float, 6> keypointcolor_to_array(const KeyPointColor& kc)
{
    return {{
        kc.keypoint.pt.x, kc.keypoint.pt.y, 0.0f,
        (float)kc.color[0], (float)kc.color[1], (float)kc.color[2]
    }};
}

int main(int argc, char* argv[])
{
    int cam = std::stoi(argv[1]);
    bool debug = argc >= 4 && std::string(argv[3]) == "debug";

    std::string img_filename;
    if (argc >= 5 && debug) {
        img_filename = std::string(argv[4]);
    }

    tracker tracker(cam, debug);
    try {
        asio::io_service io_service;
        std::string ip_str(argv[2]);
        sender s(io_service, asio::ip::address::from_string(argv[2]));
        io_service.run();

        std::cout << "start sending" << std::endl;
        while(true){
            vector<KeyPointColor> locs;
            if (img_filename.length() > 0) {
                locs = tracker.trackObjects(img_filename);
            } else {
                locs = tracker.trackObjects();
            }
            for (auto & loc : locs) {
                s.send_position(keypointcolor_to_array(loc));
            }
        }
    } catch (std::exception& e) {
        std::cerr << "Exception: " << e.what() << "\n";
    }
}
