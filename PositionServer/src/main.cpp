/*
    Positiontracker & server for ELRO Wants To Play
    Copyright (C) 2015 Niek Arends
    Copyright (C) 2015 Olaf van der Kruk
    Copyright (C) 2015 Simon Voordouw

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#include "boost/asio.hpp"
#include "boost/bind.hpp"
#include "boost/date_time/posix_time/posix_time_types.hpp"
#include <array>
#include <vector>
#include <functional>
#include <string>

#include "tracker.hpp"

namespace asio = boost::asio;

const short multicast_port = 2000;

class sender
{
public:
	/**
	*	Constructor
	*	\param io_service
	*	\param multicast_adress
	*/
    sender(asio::io_service& io_service, const asio::ip::address& multicast_address): 
		endpoint(multicast_address, multicast_port), 
		socket(io_service, endpoint.protocol())
    {
    }
	/**
	*	Sends a position
	*	\param position The position to be sent
	*/
    void send_position(std::array<float, 6> position) 
    {
        socket.async_send_to(asio::buffer(position), endpoint,
                [this](boost::system::error_code ec, std::size_t){
                    std::cout << "send message" << std::endl;
                });
    }

private:
    asio::ip::udp::endpoint endpoint;		//a variable
    asio::ip::udp::socket socket;			//a variable
};

	/**
	*	Something??
	*	\param kc Converts a keypointcolor to an array
	*/
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
            std::vector<KeyPointColor> locs;
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
