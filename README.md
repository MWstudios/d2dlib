# d2dlib Extended
This fork adds extended support for various Direct2D customisations that were impossible (or avoided for some reason) in the original branch. The biggest feature is applying gradient brushes to places where they couldn't be before.
# What's new in this extension?
## Brushes
New options "extendMode" and "gamma" allow you to change the wrapping beyond the gradient's borders and gamma correction (but that's rarely needed).
```csharp
D2DBrush brush = Device.CreateLinearGradientBrush(0f, 0f, 20f, 0f, //coordinates
    new D2DGradientStop[2] { 
    new D2DGradientStop(0f, D2DColor.Blue),
    new D2DGradientStop(1f, D2DColor.Red)
    },
    D2DGradientExtendMode.Mirror); //gradient mirrors beyond borders
```
## Text
You can set the D2DGraphics' text antialiasing. `None` turns it off, `Grayscale` enables default antialiasing and `ClearType` creates red and blue artifacts around the text's edges.
```csharp
g.TextAntialias = D2DTextAntialiasMode.Grayscale;
```
Using the linear gradient brush we defined earlier, we can draw a string with it. This was impossible in the vanilla version.
```csharp
g.DrawText("Text gradient",
    brush,
    new Font("Arial", 100), //font name and size
    0, 0, //coordinates
    DWriteTextAlignment.Leading, //horizontal alignment
    DWriteParagraphAlignment.Near, //vertical alignment
    DWriteFontWeight.Black, //boldness
    DWriteFontStyle.Italic, //italicness
    DWriteFontStretch.Normal //expanding
    );
```
![Text with a gradient brush](snapshots/text%20gradient.png)
There are another two parameters, DWriteWordWrapping and DWriteTextOptions, that apply when you specify a D2DRect instead of the drawing coordinates.

`DwriteWordWrapping` specifies what automatical line breaks it will use. `None` turns it off, `Wrap` breaks words only and `Emergency Wrap` breaks the individual letters in words.

`DWriteTextOptions` specifies how to render the parts of text that are outside of the rectangle. `None` renders them, `Clip` doesn't.
```csharp
g.DrawText("Text gradient being clipped", brush, new Font("Arial", 50),
    new D2DRect(0, 0, 300, 250), //rectangle instead of coordinates
    DWriteTextAlignment.Leading, DWriteParagraphAlignment.Near, DWriteFontWeight.Black, DWriteFontStyle.Italic, DWriteFontStretch.Normal,
    DWriteWordWrapping.EmergencyWrap, //wraps indiviudal letters
    DWriteTextOptions.Clip //clips text outside of the rectangle
    );
```
![Text being clipped](snapshots/text%20clip.png)
## Lines and outlines
⚠ Warning: These aren't supported in all methods yet.

In some methods (such as DrawPolygon) you can specify a brush instead of a solid color as the outline. You can now also set the dash styles, cap styles and the corner style.
```csharp
g.DrawPolygon(
    new D2DPoint[4] { new(100, 300), new(250, 300), new(250, 400), new(100, 400) }, //the vertices
    brush, brush2, //brush fill and brush outline
    10f, //outline width
    D2DDashStyle.Dash, //outline dash style
    D2DCapStyle.Flat, D2DCapStyle.Flat, //caps at the start and end (if the shape had one)
    D2DCapStyle.Round, //round caps between the individual dashes
    D2DLineJoinStyle.Round //round corners
    );
```
![Gradient outline](snapshots/gradient%20outline.png)
