# Specify the minimum version for CMake
cmake_minimum_required(VERSION 3.26)

# Project's name
project(ssl-aim-trainer VERSION 1.0.0 LANGUAGES CXX)

# Set the C++ standard
set(CMAKE_CXX_STANDARD 17)
set (CMAKE_CXX_STANDARD_REQUIRED ON)

# Add the path to the Qt6 installation
list(APPEND CMAKE_PREFIX_PATH "D:/Qt/6.7.1/msvc2019_64/lib/cmake/Qt6")

# Link Qt
find_package(Qt6 COMPONENTS Core REQUIRED)
qt_standard_project_setup()

qt_add_executable(${PROJECT_NAME}
    src/main.cpp
)

target_link_libraries(${PROJECT_NAME} PRIVATE Qt6::Core)
