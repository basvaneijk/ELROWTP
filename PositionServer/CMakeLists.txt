cmake_minimum_required(VERSION 3.2)
project (position-server)

message("Compiler: " ${CMAKE_CXX_COMPILER_ID})

find_package(OpenCV REQUIRED)
find_package(Boost REQUIRED COMPONENTS system)

message("Boost Include: ${Boost_INCLUDE_DIRS}")
message("Boost Library Directories: ${Boost_LIBRARY_DIRS}")
message("Boost Libraries: ${Boost_LIBRARIES}")

add_executable(position-server src/main.cpp src/tracker.cpp)
set_property(TARGET position-server PROPERTY CXX_STANDARD 11)
set_property(TARGET position-server PROPERTY CXX_STANDARD_REQUIRED 11)

if(${UNIX})
   set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wextra -pedantic -flto") 
endif()

if("${CMAKE_CXX_COMPILER_ID}" MATCHES "Clang")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -fcolor-diagnostics")
endif()

if(WIN32)
  add_definitions(-D_WIN32_WINNT=0x0501)
endif()

set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} -flto") 
include_directories(${Boost_INCLUDE_DIR}
                    ${OpenCV_INCLUDE_DIR})
link_directories(${Boost_LIBRARY_DIRS})
target_link_libraries(position-server ${Boost_LIBRARIES}
                                      ${OpenCV_LIBS})
