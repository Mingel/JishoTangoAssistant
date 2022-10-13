# JishoTangoAssistant

JishoTangoAssistant is an application which can create custom vocabulary lists for Japanese study purposes with the help of the [jisho.org API](https://jisho.org/forum/54fefc1f6e73340b1f160000-is-there-any-kind-of-search-api). These list can then be exported to Anki (via CSV files).

## Screenshots
![image](https://user-images.githubusercontent.com/46728839/195469359-0258e6dd-7800-4847-bbbb-f83f35c42408.png)
![image](https://user-images.githubusercontent.com/46728839/195469549-1d94e6de-8581-4459-a9da-3a06a3264673.png)


## Build
[.NET 6 SDK](https://dotnet.microsoft.com/download) must be installed.

To build and run the application, use the following commands from the root folder:
```
cd JishoTangoAssistant
dotnet run
```
Alternatively, you can use an IDE like e.g. [Visual Studio](https://visualstudio.microsoft.com/vs/) to build and run the application, or for further development.

## Credits
Thanks to the team from [jisho.org](https://jisho.org/) for making this possible!

Jisho.org uses several data sources, which can be found at jisho.org's [About Page](https://jisho.org/about). Relevant results from jisho.org are taken from [JMdict](http://www.edrdg.org/wiki/index.php/JMdict-EDICT_Dictionary_Project) and [JMnedict](http://www.edrdg.org/enamdict/enamdict_doc.html).
