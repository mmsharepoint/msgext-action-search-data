console.log("Script loader before?");
(function (SbmData) {
  
  SbmData.getContext = function () {
    if (microsoftTeams) {
      microsoftTeams.initialize();
    }
    console.log("Script loades");
    
  }
  
  SbmData.submitTask = function (Id, Name, Orders, Orderable) {
    if (microsoftTeams) {
      microsoftTeams.initialize();
      microsoftTeams.tasks.submitTask({ id: Id, name: Name, orders: Orders, oderable: Orderable });
    }
  }
  
}(window.SbmData = window.SbmData || {}));