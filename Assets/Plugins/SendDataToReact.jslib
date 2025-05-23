mergeInto(LibraryManager.library, {
  SendDataToReact: function (strPtr) {
    var message = UTF8ToString(strPtr);
    console.log("Mensaje recibido de Unity:", message);

    if (window.handleUnityMessage) {
      window.handleUnityMessage(message);
    }
  }
});