cmake_minimum_required(VERSION 3.26)

set(VERSION 1.0.0)

# Project's name
if (WIN32)
    project(ssl-aim-trainer VERSION ${VERSION} LANGUAGES CXX)
elseif(UNIX)
    project(ssl-aim-trainer VERSION ${VERSION})
endif()

set(CMAKE_CONFIGURATION_TYPES "Debug;Release" CACHE STRING "" FORCE)

# Set the C++ standard
set(CMAKE_CXX_STANDARD 23)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Correct the typo and set Qt paths
set(CMAKE_INCLUDE_CURRENT_DIR ON)
set(CMAKE_AUTOMOC ON)
set(CMAKE_PREFIX_PATH $ENV{QTDIR})
find_package(Qt6Core REQUIRED)
find_package(Qt6Widgets REQUIRED)

# Add source dirs
set(INCLUDE_DIR ${CMAKE_SOURCE_DIR}/include)
set(SOURCE_DIR ${CMAKE_SOURCE_DIR}/src)

include_directories(${INCLUDE_DIR})

file(GLOB_RECURSE SOURCES
    ${INCLUDE_DIR}/*.h
    ${SOURCE_DIR}/*.cpp
)

# Add executable
if (WIN32)
    add_executable(${PROJECT_NAME} WIN32 ${SOURCES})
elseif(UNIX)
    add_executable(${PROJECT_NAME} ${SOURCES})
endif()

# Add target includes
target_include_directories(${PROJECT_NAME} PRIVATE ${INCLUDE_DIR})
target_include_directories(${PROJECT_NAME} PRIVATE ${SOURCE_DIR})

# Link Qt
target_link_libraries(${PROJECT_NAME} Qt6::Widgets)
