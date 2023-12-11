# Events

Seq

```http request
http://localhost:81
```

Back-End

```http request
https://localhost:7095
```

Briefly describe what this project does and the problem it solves.

## Getting Started

This section should describe how to install and get started with your project. If
applicable, this can include requirements, installation steps, etc.

### Prerequisites

List any libraries and other dependencies required to run this project.

### Installation

Explain how to install and set up your project.

## Usage

Provide code examples, screenshots, or demos to show how to use your project.

## Running Tests

Explain how to run the automated tests for this project.

## Deployment

- For Windows: The following will need to be executed from your terminal to create a
  cert `dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Your_password123 dotnet dev-certs https --trust`
    - NOTE: When using PowerShell, replace %USERPROFILE% with $env:USERPROFILE.

- FOR macOS: `dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p
  Your_password123 dotnet dev-certs https --trust`

- FOR Linux: `dotnet dev-certs https -ep ${HOME}/.aspnet/https/aspnetapp.pfx -p
  Your_password123`

create .env variable for Docker

``` sh
chmod +x init.sh
./init.sh
```

## Built With

List any major frameworks or libraries used in your project.

## Contributing

Please read [CONTRIBUTING.md](link to contribution guidelines) for details on our code of
conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see
the [tags on this repository](link to project tags).

## Authors

* **Your Name** - *Initial work* - [YourGitHub](link to your GitHub)

See also the list of [contributors](link to project contributors page) who participated in
this project.

## License

This project is licensed under the [LICENSE.md](link to the license) you find in the
repository.

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc



