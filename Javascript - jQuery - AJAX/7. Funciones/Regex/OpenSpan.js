var step = -1;
parent.opportunityCreated = false;
parent.opportunityCheckStatus = false;
parent.customerCountry = '';

function OpenSpan_GetID(regEx, text) {

  var re = new RegExp(regEx);
  var m = re.exec(text);

  if (m == null) {
    return '';
  }
  else {
    return m[0];
  }
}

function htmlbSubmitLib(library, elem, eventType, formID, objectID, eventName, paramCount, param1, param2, param3, param4, param5, param6, param7, param8, param9) {

  if (eventName == 'IC_BT_SVT' && step == -1) {
    var oReg = objectID.substring(10);
    if (oReg == '_IC_BT_SVT') {
      step = 0;
    }
  }

  if (eventName == 'confirm') {
    var form = document.getElementById(formID);
    var buttonID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9]_V[0-9][0-9]_V[0-9][0-9]_Confirm', form.innerHTML);

    if (objectID == buttonID) {

      var textID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9]_V[0-9][0-9]_V[0-9][0-9]_defaultaddress_struct.country', form.innerHTML);
      var text = document.getElementById(textID);

      if (text == null) {
        //alert('Could not find element with id ' + textID);
      }
      else {
        parent.customerCountry = text.value; //parent is "FRAME_APPLICATION"
      }
    }
  }

  if (eventName == 'save') {
    var form = document.getElementById(formID);
    var buttonID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9][0-9]_V[0-9][0-9][0-9]_thtmlb_button_1 ', form.innerHTML);

    if (objectID == buttonID) {

      var textID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9][0-9]_V[0-9][0-9][0-9]_V[0-9][0-9][0-9]_V[0-9][0-9][0-9]_btstatus_struct.act_status', form.innerHTML);
      var text = document.getElementById(textID);

      if (text == null) {
        //alert('Could not find element with id ' + textID);
      }
      else {
        parent.opportunityPreviousStatus = parent.opportunityStatus;
        parent.opportunityStatus = text.value; //parent is "FRAME_APPLICATION"
        parent.opportunityCheckStatus = true;
      }
    }
  }

  document.forms[formID].onInputProcessing.value = library;
  htmlbSubmit(elem, eventType, formID, objectID, eventName, paramCount, param1, param2, param3, param4, param5, param6, param7, param8, param9);

}

function OpenSpan_SelectValueForDropDown(fieldId, value) {

  var buttonId = fieldId + '-btn';
  var button = document.getElementById(buttonId);

  if (button == null) {
    alert('Could not find element with id ' + buttonId);
  }

  //alert('about to button.click');
  button.click();

  var popupDivId = fieldId + '__items';
  var popupDiv = document.getElementById(popupDivId);

  if (popupDiv == null) {
    alert('Error setting drop down field : Could not find items entry with id ' + popupDivId);
  }

  var listItems = popupDiv.getElementsByTagName('LI');

  if (listItems == null) {
    alert('Error setting drop down field : Could not find items for id ' + fieldId);
  }

  if (listItems.length == 0) {
    alert('Error setting drop down field : No entries found in the items list for ' + fieldId);
  }

  //Trim any leading or trailing whitespace from the value
  value = value.replace(/^\s*/, '').replace(/\s*$/, '');

  for (var i = 0; i < listItems.length; i++) {
    var item = listItems[i];

    //Trim any leading or trailing whitespace from the item text
    var itemValue = item.innerText.replace(/^\s*/, '').replace(/\s*$/, '');

    if (itemValue == value) {
      thtmlb_processSelection(item, '');
      return;
    }
  }

  alert('Error setting drop down field : No entries found that match the given value');
}

function OpenSpan_CreateOpportunity1() {

  var form = document.getElementById('myFormId');

  var textID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9]_V[0-9][0-9]_btsubject7_struct.codetext', form.innerHTML);
  var text = document.getElementById(textID);

  if (text == null) {
    //alert('Error, Could not find entry with id ' + textID);
    step = -1;
    return;
  }

  /*
  if (text.value != 'Out Of Warranty') {
  alert('text.value: ' + text.value);
  return;
  }
  */

  var linkID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9]_V[0-9][0-9]_V[0-9][0-9]_FolUp', form.innerHTML);
  var link = document.getElementById(linkID);

  if (link == null) {
    //alert('Error, Could not find entry with id ' + linkID);
    step = -1;
    return;
  }

  step = 1;

  //alert('OpenSpan_CreateOpportunity1 - about to link.click');
  link.click();
}

function OpenSpan_CreateOpportunity2() {

  var form = document.getElementById('myFormId');

  var textID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9]_V[0-9][0-9]_createfollowup_bttype', form.innerHTML);

  var text = document.getElementById(textID);

  if (text == null) {
    //alert('Error, Could not find entry with id ' + textID);
    return;
  }

  step = 2;

  //alert('OpenSpan_CreateOpportunity2 - about to OpenSpan_SelectValueForDropDown');
  OpenSpan_SelectValueForDropDown(textID, 'HP Opportunity');

}

function OpenSpan_CreateOpportunity3() {

  var form = document.getElementById('myFormId');

  var buttonID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9]_V[0-9][0-9]_ButtonCreateFollowUp', form.innerHTML);
  var button = document.getElementById(buttonID);

  if (button == null) {
    //alert('Error, Could not find entry with id ' + buttonID);
    step = -1;
    return;
  }

  step = -1;

  parent.opportunityCreated = true; //parent is "FRAME_APPLICATION"
  parent.opportunityStatus = ''; //parent is "FRAME_APPLICATION"

  //alert('OpenSpan_CreateOpportunity3 - about to button.click');
  button.click();
  //return htmlbSL(this, 2, buttonID + ':CreateFollowUp', '0');
}

function onStateChange() {

  var readyState = this.request.readyState;

  if (readyState != 1)
    this.respondToReadyState(this.request.readyState);

  if (readyState == 4 && step == 0 && parent.opportunityCreated == false)
    OpenSpan_CreateOpportunity1();

  if (readyState == 4 && step == 1)
    OpenSpan_CreateOpportunity2();

  if (readyState == 4 && step == 2)
    OpenSpan_CreateOpportunity3();

  if (readyState == 4 && parent.opportunityCheckStatus == true) {

    var form = document.getElementById('myFormID');
    var buttonID = OpenSpan_GetID('C[0-9][0-9]_W[0-9][0-9][0-9]_V[0-9][0-9][0-9]_thtmlb_button_1 ', form.innerHTML);
    var button = document.getElementById(buttonID);

    if (button == null) {
      //alert('Error, Could not find entry with id ' + buttonID);
    }
    else {
      if (button.onclick.toString().indexOf('return false;') == -1) {
        //error occurred, status not changed
        parent.opportunityStatus = parent.opportunityPreviousStatus;
      }
      //else saved successfully
    }
    parent.opportunityCheckStatus = false;
  }


  //alert('onStateChange ' +  readyState.toString());
}

//OpenSpan_CreateOpportunity1();
