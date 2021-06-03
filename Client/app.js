//JS Functions Page :)

// ------------------------- LOGIN -------------------------

//login check
async function loginCheck() {
    //get values from input
    const obj = {   username : document.getElementById("username").value, 
                    password : document.getElementById("password").value};

    const fetchParams = {
        method : 'post',
        body : JSON.stringify(obj),
        headers : {"Content-type" : "application/json"}
        }
        const res = await fetch("https://localhost:44351/api/Login", fetchParams);
        const data = await res.json(); 
        console.log(data);
    //check if user exist or not
    if(data != null) {
        sessionStorage.setItem("username", data.username);
        sessionStorage.setItem("fullname", data.fullname);
        sessionStorage.setItem("userID", data.ID);
        sessionStorage.setItem("numOfActions", data.numOfActions);
        sessionStorage.setItem("actions", 0);

        window.location.href = "index.html?user=" + data.ID;
    } else {alert("worng user / pass")};
}

function logoutBtn() {
    //clear browser session
    sessionStorage.clear();
    window.location.href = "login.html";
}

// ------------------------- LOGs CHECK -------------------------
function addAction() {
    let actionsSoFar = Number(sessionStorage.getItem("actions"));
    actionsSoFar ++;
    sessionStorage.setItem("actions", actionsSoFar);
    checkActions();
}

function checkActions() {
    const actionsSoFar = Number(sessionStorage.getItem("actions"));
    const numOfActions = Number(sessionStorage.getItem("numOfActions"));
    if(actionsSoFar >= numOfActions) {
        window.location.href = "login.html";
    }
}