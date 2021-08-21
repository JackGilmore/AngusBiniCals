# AngusBiniCals

## About the project

<p float="left">
  <img src="https://github.com/JackGilmore/AngusBiniCals/blob/master/.github/images/scrn_main_page.png" width="49%" />
  <img src="https://github.com/JackGilmore/AngusBiniCals/blob/master/.github/images/scrn_bin_list.png" width="49%" />
</p>


A web app which scrapes bin collection calendar information from the [Angus Council](https://angus.gov.uk) website and serves it as an iCal feed, primarily for subscription and reminders on your smartphone.

### Built with
- .NET 5.0
- Bootstrap
- HtmlAgilityPack
- RestSharp
- ical.NET

## Getting started
To get a local copy up and running, follow these steps:

### Prerequisites

You should have an appropriate IDE installed to work with .NET 5 C# code. I usually use Visual Studio but other IDEs are available (Rider, VS Code etc.)

Once you have cloned the project and opened in your IDE, ensure you have installed all the relevant packages by running
```ps
dotnet restore
```
Then to start the project, run
```ps
dotnet run
```

The application should then be accessible from https://localhost:5001 (or similar if you have configured a different default port)

## Roadmap and contributions

See the [open issues](https://github.com/JackGilmore/AngusBiniCals/issues) for a list of proposed features and known issues. Feel free to contribute some yourself!

Contributions are both welcome and greatly appreciated. To contribute to the project, follow these instructions:
1. Fork the repository
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request
