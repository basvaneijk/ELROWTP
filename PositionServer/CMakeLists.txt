cmake_minimum_required(VERSION 3.2)
project (position-server)

set(OpenCV_STATIC_ON)
set(Boost_USE_STATIC_LIBS ON)

find_package(OpenCV REQUIRED)
find_package(Boost REQUIRED COMPONENTS system)


add_executable(position-server src/main.cpp src/tracker.cpp)
set_property(TARGET position-server PROPERTY CXX_STANDARD 11)
set_property(TARGET position-server PROPERTY CXX_STANDARD_REQUIRED 11)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -pedantic") 

if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "Clang")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fcolor-diagnostics")
endif()

set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} -flto") 
include_directories(${BOOST_INCLUDE_DIR}
                    ${OpenCV_INCLUDE_DIR})
target_link_libraries(position-server ${Boost_LIBRARIES}
                                      ${OpenCV_LIBS})

add_executable(position-client src/client.cpp)
set_property(TARGET position-client PROPERTY CXX_STANDARD 11)
set_property(TARGET position-client PROPERTY CXX_STANDARD_REQUIRED 11)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -pedantic") 

if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "Clang")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fcolor-diagnostics")
endif()

set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} -flto") 
include_directories(${BOOST_INCLUDE_DIR}
                    ${OpenCV_INCLUDE_DIR})
target_link_libraries(position-client ${Boost_LIBRARIES}
                                      ${OpenCV_LIBS})
