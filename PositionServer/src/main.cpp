#include "boost/asio.hpp"
#include "boost/bind.hpp"
#include "boost/date_time/posix_time/posix_time_types.hpp"
#include <array>

#include "tracker.hpp"

using namespace boost::asio;

const short multicast_port = 2000;
const int max_message_count = 10;

struct PositionUpdate {
    float position[3];
    float color[3];
};

class sender
{
public:
    sender(boost::asio::io_service& io_service,
            const boost::asio::ip::address& multicast_address)
        : endpoint_(multicast_address, multicast_port),
        socket_(io_service, endpoint_.protocol()),
        timer_(io_service),
        message_count_(0)
    {
        std::cout << "sending to ip: " << multicast_address << std::endl;
        message_ = 
        {{ (float)message_count_++, 50.0f, 51.0f,
           99.0f, 98.0f, 97.0f
        }};
    
        socket_.async_send_to(
                boost::asio::buffer(message_, sizeof(message_)), endpoint_,
                boost::bind(&sender::handle_send_to, this,
                    boost::asio::placeholders::error));
    }

    void handle_send_to(const boost::system::error_code& error)
    {
        if (!error && message_count_ < max_message_count)
        {
            std::cout << "Send message" << std::endl;
            timer_.expires_from_now(boost::posix_time::seconds(5));
            timer_.async_wait(
                    boost::bind(&sender::handle_timeout, this,
                        boost::asio::placeholders::error));
        }
    }

    void handle_timeout(const boost::system::error_code& error)
    {
        if (!error)
        {
            message_ = {{(float)message_count_++, 50.0f, 51.0f,
                         99.0f, 98.0f, 97.0f }};

            socket_.async_send_to(
                    boost::asio::buffer(message_, sizeof(message_)), endpoint_,
                    boost::bind(&sender::handle_send_to, this,
                        boost::asio::placeholders::error));
        }
    }

private:
    boost::asio::ip::udp::endpoint endpoint_;
    boost::asio::ip::udp::socket socket_;
    boost::asio::deadline_timer timer_;
    int message_count_;
    std::array<float, 6> message_;
};

int main(int argc, char* argv[])
{
    try {
        io_service io_service;
        sender s(io_service, ip::address::from_string(argv[1]));
        io_service.run();
    } catch (std::exception& e) {
        std::cerr << "Exception: " << e.what() << "\n";
    }
}
