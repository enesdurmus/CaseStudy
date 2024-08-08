# Case

This project is a C# console application for Software Engineering Case.

## Requirements

- .NET 6 SDK: You need to have .NET 6 SDK installed to build and run the project.

## Usage

1. Clone the project files to your local machine.
2. Navigate to the project's root directory in the console.
3. Build the project using the command `dotnet build`.
4. After the build is complete, run the application using the command `dotnet run`.

## Dependencies

- This project uses the Newtonsoft.Json library for JSON operations. The library version used is 13.0.3.

## Input Data

- The `input.json` file should be placed in the `Data` folder located at the root of the project.
- After the project is built or run, the `input.json` file will be copied to the `bin/Debug/net6.0/Data` directory.
- If you run the application directly from the `bin/Debug/net6.0` directory, it will use the `input.json` file that has already been copied there. This allows you to modify the input file in the `bin/Debug/net6.0/Data` directory without rebuilding the project.

## Output Data

- The output JSON file, `output.json`, will be generated in the `bin/Debug/net6.0/Data` directory after the project is built and run.
