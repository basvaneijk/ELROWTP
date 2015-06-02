cmake_minimum_required(VERSION 3.2)
project (position-server)

find_package(OpenCV REQUIRED)
find_package(Boost REQUIRED COMPONENTS system)

message(status "** Boost Include: ${Boost_INCLUDE_DIRS}")
message(status "** Boost Libraries: ${Boost_LIBRARY_DIRS}")
message(status "** Boost Libraries: ${Boost_LIBRARIES}")


add_executable(position-server src/main.cpp src/tracker.cpp)
set_property(TARGET position-server PROPERTY CXX_STANDARD 11)
set_property(TARGET position-server PROPERTY CXX_STANDARD_REQUIRED 11)


# Set default compile flags for GCC
if(CMAKE_COMPILER_IS_GNUCXX)
   set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -pedantic") 

endif(CMAKE_COMPILER_IS_GNUCXX)

if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "Clang")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fcolor-diagnostics")
endif()

set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} -flto") 
include_directories(${Boost_INCLUDE_DIR}
                    ${OpenCV_INCLUDE_DIR})
LINK_DIRECTORIES(${Boost_LIBRARY_DIRS})
target_link_libraries(position-server ${Boost_LIBRARIES}
                                      ${OpenCV_LIBS})