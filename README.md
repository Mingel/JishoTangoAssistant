# JishoTangoAssistant

JishoTangoAssistant is an application which can create custom vocabulary lists for Japanese study purposes with the help of the [jisho.org API](https://jisho.org/forum/54fefc1f6e73340b1f160000-is-there-any-kind-of-search-api). These list can then be exported to Anki (via CSV files).

## Screenshots
![image](https://user-images.githubusercontent.com/46728839/214991881-f010e411-fd4f-4ae5-95d7-4c2391499a20.png)
![image](https://user-images.githubusercontent.com/46728839/214991961-be0e5158-b9ac-4f1e-a3f0-3aec29aba89a.png)

## Build
[.NET 9 SDK](https://dotnet.microsoft.com/download) must be installed.

To build and run the application, use the following commands from the root folder:
```
cd JishoTangoAssistant
dotnet run
```
Alternatively, you can use an IDE like e.g. [Visual Studio](https://visualstudio.microsoft.com/vs/) to build and run the application.

## Credits
Thanks to the team from [jisho.org](https://jisho.org/) for making this possible!

Jisho.org uses several data sources, which can be found at jisho.org's [About Page](https://jisho.org/about). Relevant results from jisho.org are taken from [JMdict](http://www.edrdg.org/wiki/index.php/JMdict-EDICT_Dictionary_Project) and [JMnedict](http://www.edrdg.org/enamdict/enamdict_doc.html).
