// std
#include <iostream>

// qt
#include <QString>
#include <QDebug>
#include <QProcess>

// defines
constexpr const char* SSL_PROCESS_ID = "ShellShockLive";

bool is_process_running(const QString& name) 
{
    QProcess process;

    process.start("ps -A -o comm=");
    process.waitForFinished();

    QString output(process.readAllStandardOutput());
    QStringList list = output.split('\n');

    bool isRunning = list.filter(name).size() > 0;

    if (isRunning)
        qInfo() << "Process" << name << "is running.";
    else
        qInfo() << "Processs" << name << "is not running.";

    return isRunning;
}

int main() 
{
    qInfo() << "Started SSL Aim Trainer";

    if (!is_process_running(SSL_PROCESS_ID))
        return 0;

    


    return 0;
}
