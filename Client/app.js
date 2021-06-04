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
        const resLog = await fetch("https://localhost:44351/api/Login/" + data.ID);
        const dataLog = await resLog.json();
        //check if user allowed to enter
        if(dataLog){
        window.location.href = "index.html?userID=" + data.ID;
        } else {alert("too many actions per day")}
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

// ------------------------- EMPLOYEES -------------------------
async function getAllEmployeesData() {
    const tablePlace = document.getElementById("tablePlace");

    const res = await fetch("https://localhost:44351/api/Employees");
    const data = await res.json();
    console.table(data);

    for (let d of data) {
        const newRow = tablePlace.insertRow();
        const fullName = newRow.insertCell().innerText = d.fullname;
        const startWorkYear = newRow.insertCell().innerText = d.startWorkYear;
        const departmentID = newRow.insertCell().innerText = d.departmentID;
        const shifts = newRow.insertCell();
        // const child = document.createElement("ul").setAttribute("id", "shiftListEmp" + d.id);
        // shifts.appendChild(child);
        // for (let s in d.empShifts) {
        //     document.getElementById("shiftListEmp" + d.id).appendChild(document.createElement("li").innerText = s.id);
        // }
    }
}