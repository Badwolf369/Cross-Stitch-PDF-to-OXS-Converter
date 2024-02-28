@startuml
skinparam packageStyle Frame


package Avalonia {
    interface Data.Converters.IValueConverter {}
    class Controls.Window {}
}

package MVVMToolkit {
    class CommunityToolkit.Mvvm.ComponentModel.ObservableObject {}
}

package System {
    interface ComponentModel.INotifyPropertyChanged {}
}

package PatternSeer {
    class Program {
        Chart : Chart
        Main(String[]:args)
    }
    class App {

    }
    class Util {
        +validPdfFile(pdfAddress:String) : Boolean
    }
}

package PatternSeer.Converters {
    class MatToBitmapConverter {
        +Convert() : Object
        +ConvertBack() : Object
    }
}

package PatternSeer.Models {
    class Chart {
        ..Fields..
        -pdfPath : String
        ..Properties..
        +Key : ChartKey <<g/-s>>
        +PageCount : Int <<g/-s>>
        +Pattern : ChartPattern <<g/-s>>
        +PdfPages : List<Mat> <<g/s>>
        ==Constructors==
        +Chart()
        ..Private Methods..
        ..Public Methods..
        +ImportPdf(path: String)
    }
    class ChartPattern {
        ..Fields..
        -unkeyedGrid : List<List<Mat>>
        -keyedGrid : List<List<KeySymbol>>
        ..Properties..
        +Size : Tuple<int, int> >= (0, 0) <<g/-s>>
        ==Constructors==
        +ChartPattern(unkeyedPattern: List<List<Mat>>, key:ChartKey)
        ..Private Methods..
        ..Public Methods..
        +GetKeySymbolAt(x: int, y: int) : KeySymbol
        +KeyGrid(key:ChartKey)
    }
    class ChartKey {
        -<o> Symbols : List<KeySymbol>
        +ChartKey(source:Mat)
        +MatchSymbol(image:Mat) : *KeySymbol
    }
    class KeySymbol {
        -<o> Image : Mat
        -<o> Color : String
        -<o> Strands : Integer = 2
        -<i/o> Count : Integer[0..1]
        -<o> Brand : String = "DMC"
        +KeySymbol(image:Mat)
    }
}

package PatternSeer.ViewModels {
    class MainViewModel {
        ..Fields..
        _chart : Chart
        _filePickerSemaphore : SemaphoreSlim
        ..Properties..
        ..Observable Properties..
        IsPdfPickerOpen : Boolean
        PdfFilePath : String
        PdfPages : ObservableCollection<Mat>
        PdfZoomLevel : String
        VisiblePdfPages : String
        ==Constructors==
        MainViewModel()
        ..Private Methods..
        ..Public Methods..
        OnViewModelUpdate(sender: object, e: PropertyChangedEventArgs)
        ..ICommands..
        ImportFromPdfCommand()
    }
}

package PatternSeer.Views {
    class MainWindow {}
}

MatToBitmapConverter .|> IValueConverter

MainViewModel --> Chart
MainViewModel -|> ObservableObject
MainViewModel ..|> INotifyPropertyChanged

MainWindow -|> Window

MainWindow <..> MainViewModel : Binding

Chart --> ChartPattern
Chart --> ChartKey
ChartPattern --> KeySymbol
ChartKey --> KeySymbol

MatToBitmapConverter <-- MainWindow


@enduml
