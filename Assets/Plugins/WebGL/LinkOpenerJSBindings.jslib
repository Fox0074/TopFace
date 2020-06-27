var LinkOpenerLib = {
    openURL: function (url) {
        var url = Pointer_stringify(url);
        window.open(url, "_blank");
    },
};

mergeInto(LibraryManager.library, LinkOpenerLib);
