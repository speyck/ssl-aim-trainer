// qt
#include <QApplication>
#include <QWidget>

// lib
#include "forms/MainWindow.h"

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
    
    QWidget window;
    window.resize(250, 150);
    window.setWindowTitle("Simple Qt Application");
    window.show();

    return app.exec();
}

