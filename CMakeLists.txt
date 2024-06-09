# Specify the minimum version for CMake
cmake_minimum_required(VERSION 3.26)

# Set the C++ standard
set(CMAKE_CXX_STANDARD 17)

# Project's name
project(ssl-aim-trainer)

# Add executable
add_executable(${PROJECT_NAME} src/main.cpp)
