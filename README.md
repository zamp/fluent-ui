# fluent-ui
A fluent interface for unity UI

https://user-images.githubusercontent.com/1029645/226439312-9d42e200-95f0-4e3f-a915-f4d5e3d9ba79.mp4

Code that was used to make what's in the video:

```cs
UIRoot.Canvas()
    .ScreenSpaceOverlay()
        .Children(
            x => x.Panel()
                .Size(new Vector2(400,200))
                .Center()
                .VerticalGroup()
                    .Children(
                        y => y.Label("This is a label"),
                        y => y.Button()
                            .PreferredHeight(20)
                            .OnClick(() => _windowReference.Open())
                            .Label("Reopen draggable window").Align(TextAlignmentOptions.Center),
                        y => y.Label(_labelBinding),
                        y => y.Dropdown("Dropdown:", _dropdownSelectionBinding, new []{"First example", "Second example", "Third example"})
                            .PreferredHeight(20)
                            .OnSelectionChanged(i => Debug.Log($"Selection changed: {i}")),
                        y => y.Toggle("Toggle", _toggleBinding)
                            .PreferredHeight(20),
                        y => y.Slider("Slider: ", _sliderBinding)
                            .PreferredHeight(20)
                    ),
            x => x.Window("Draggable window")
                .Position(400,400)
                .Size(200,100)
                .Out(out _windowReference)
                .VerticalGroup(GroupForceExpand.Both)
                    .Fill()    
                    .Image(_imageBinding)
        );
```

# How to install
Use unity package manager and add from github url.

Enter https://github.com/zamp/fluent-ui.git to it.

There is also an example in the package you can import it by clicking "Import" next to the example.

![image](https://user-images.githubusercontent.com/1029645/226439820-616fa401-a6a0-4e66-88c0-6e233c1e33b8.png)
