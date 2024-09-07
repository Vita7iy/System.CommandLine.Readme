# System.CommandLine.Readme

**System.CommandLine.Readme** extension for the **System.CommandLine** library to create a readme file in MarkDown format from a prepared command line configuration.

## How to use

1. Add code

    ```C#
    rootCommand.AddCommandLineReadmeToRoot();
    ```

2. Run application with arguments

    ```bash
    <app.exe> rm -md Readme.md
    ```