function initElements() {
  var elements = document.getElementsByClassName("leftPanelElementBlock");
  for (element of elements) {
    element.addEventListener("click", selectElement);
  }
  if (document.getElementById("hiElementId") && document.getElementById("hiElementId").value == ""){
    document.getElementById("hiElementId").value = elements[0].id;
    let button = document.getElementById("btnCallBack");
    button.click();
  }

  initSelector();
}

function selectElement() {
  document.getElementById("hiElementId").value = this.id;

  let button = document.getElementById("btnCallBack");
  button.click();
}

function initSelector() {
  const selectElement = document.getElementById("selector");

  if (selectElement) {
    selectElement.addEventListener('change', (event) => {
      document.getElementById("hiSelect").value = event.target.value;
      document.getElementById("btnCallBackSelect").click();
    });
  }
}