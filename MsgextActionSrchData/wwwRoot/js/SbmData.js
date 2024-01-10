console.log("Script loader before?");
(function (SbmData) {
  
  SbmData.getContext = function () {
    if (microsoftTeams) {
      microsoftTeams.initialize();
    }
    console.log("Script loades");
  }
  SbmData.submitTask = function () {
    if (microsoftTeams) {
      microsoftTeams.dialog.submit(
        {
          Id: "1",
          Name: "Produc1",
          Orderable: true,
          Orders: 18
        }
      )
    };
  }
  
}(window.SbmData = window.SbmData || {}));