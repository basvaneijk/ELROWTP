cmake_minimum_required(VERSION 3.2)
project (position-server)

find_package(OpenCV REQUIRED)
find_package(Boost REQUIRED COMPONENTS iostreams)

file(GLOB SOURCES src/*.cpp src/*.c)

add_executable(position-server ${SOURCES})
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
