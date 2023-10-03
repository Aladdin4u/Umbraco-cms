const url = "http://localhost:31542/Umbraco/Api/Auth/Login";
async function postJSON(data) {
  try {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    const result = await response.text();
    const splitString = result.split(">")[5].replace("</return", "");
    const parseString = JSON.parse(splitString);
    const display = document.getElementById("response");
    if (parseString.ResultCode == -1) {
      display.textContent = parseString.ResultMessage;
      display.classList.add("alert-danger");
      return;
    }
    display.textContent = `Login Successfully:\n ${splitString}`;
    display.classList.add("alert-success");
    return;
  } catch (error) {
    console.error("Error:", error);
  }
}

document.getElementById("login").addEventListener("submit", (e) => {
  e.preventDefault();
  const username = document.getElementsByName("UserName");
  const password = document.getElementsByName("Password");
  const data = { Username: username[0].value, Password: password[0].value };
  postJSON(data);
});
