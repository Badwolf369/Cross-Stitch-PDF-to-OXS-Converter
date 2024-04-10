@startuml
skinparam packageStyle Frame

legend top
    |MVVM Layers| Description|
    |<#DE473B> Models| Pure Logic 100% unrelated to the GUI|
    |<#108123> Views| GUI structure, functions the directly\n change GUI|
    |<#305992> ViewModels| Logic that indirectly interacts with GUI\n via data binding & ICommands|
    |<#DEA13B> Overseers| Oversees the entire program to define\n bindings between Views & ViewModels|
endlegend

class Program #DEA13B;line:6B4300 {
    Chart : Chart
    Main(String[]:args)
}
class App #DEA13B;line:6B4300 {}
note top of App
    As an Overseer,
    App will instantiate
    Views and ViewModels
    and will define coupling
    between them. Only <b>one</b>
    View and <b>one</b> ViewModel
    are to be coupled at a
    time.
end note

package Model_Layer {
    class Chart #DE473B;line:6B0800 {
        ..Fields..
        -pdfPath : String
        ..Properties..
        +Key : ChartKey <<g/-s>>
        +PageCount : Int <<g/-s>>
        +Pattern : ChartPattern <<g/-s>>
        +PdfPages : List<Mat> <<g/s>>
        ====
        ..Private Methods..
        ..Constructors..
        +Chart()
        ..Public Methods..
        +ImportPdf(path: String)
    }
    class ChartPattern #DE473B;line:6B0800 {
        ..Fields..
        -unkeyedGrid : List<List<Mat>>
        -keyedGrid : List<List<KeySymbol>>
        ..Properties..
        +Size : Tuple<int, int> >= (0, 0) <<g/-s>>
        ====
        ..Private Methods..
        ..Constructors..
        +ChartPattern(unkeyedPattern: List<List<Mat>>, key:ChartKey)
        ..Public Methods..
        +GetKeySymbolAt(x: int, y: int) : KeySymbol
        +KeyGrid(key:ChartKey)
    }
    class ChartKey #DE473B;line:6B0800 {
        -<o> Symbols : List<KeySymbol>
        +ChartKey(source:Mat)
        +MatchSymbol(image:Mat) : *KeySymbol
    }
    class KeySymbol #DE473B;line:6B0800 {
        -<o> Image : Mat
        -<o> Color : String
        -<o> Strands : Integer = 2
        -<i/o> Count : Integer[0..1]
        -<o> Brand : String = "DMC"
        +KeySymbol(image:Mat)
    }
}

package ViewModel_Layer {
    class MainViewModel #305992;line:163B6F {
        ..Fields..
        -_chart : Chart
        -_filePickerSemaphore : SemaphoreSlim
        ..Properties..
        ..Observable Properties..
        +IsPdfPickerOpen : Boolean
        +PdfFilePath : String
        +PdfPages : ObservableCollection<Mat>
        +PdfZoomLevel : String
        +VisiblePdfPages : String
        ====
        ..Private Mothods..
        ..Constructors..
        +MainViewModel()
        ..Public Methods..
        +OnViewModelUpdate(sender: object, e: PropertyChangedEventArgs)
        ..ICommands..
        +ImportFromPdfCommand()
    }
}

package View_Layer {
    package Converters {
        class MatToBitmapConverter #108123;line:00510E {
            +Convert() : Object
            +ConvertBack() : Object
        }
    }
    class ViewUtils #108123;line:00510E {}
    class Desktop.MainWindow #108123;line:00510E {}
    class Mobile.MainView #108123;line:00510E {}
}

ViewUtils <-- MainView
ViewUtils <-- MainWindow
MatToBitmapConverter <-- MainWindow

MainWindow <..> MainViewModel #line:red;text:red : Data Binding
MainView <..> MainViewModel #line:red;text:red : Data Binding

Program -> App
App -[hidden] MainViewModel

MainViewModel --* "1" Chart
Chart --* "1" ChartPattern
Chart --* "1" ChartKey
ChartPattern --o "1..*" KeySymbol
ChartKey --* "1..*" KeySymbol

@enduml
