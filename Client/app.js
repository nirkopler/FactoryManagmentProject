//JS Functions Page :)

window.onload = () => {
    const tablePlace = document.getElementById("tablePlace");
}

//Get User ID On Each Page
const urlParams = new URLSearchParams(window.location.search);
const userId = urlParams.get('userId');
const empId = urlParams.get('empId');
console.log("userId: " + userId);
console.log("empId: " + empId);

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
        window.location.href = "index.html?userId=" + data.ID;
        } else {alert("too many actions per day")}
    } else {alert("worng user / pass")};
}

function logoutBtn() {
    window.location.href = "login.html";
}

// ------------------------- EMPLOYEES -------------------------
//get all data of employees and create action buttons
async function getAllEmployeesData() {
    const res = await fetch("https://localhost:44351/api/Employees/" + userId);
    const data = await res.json();
    console.table(data);
    if(data != null){
        //create row for each employee
        for (let d of data) {
            const newRow = tablePlace.insertRow();
            const fullName = newRow.insertCell().innerText = d.fullname;
            const startWorkYear = newRow.insertCell().innerText = d.startWorkYear;
            const departmentName = newRow.insertCell().innerText = d.departmentName;
            const isManager = newRow.insertCell().innerText = d.isManager;
            const shifts = newRow.insertCell();
            const child = document.createElement("ul");
            shifts.appendChild(child);
            //create shifts for each epmloyee
            for (let s of d.empShifts) {
                const newShift = document.createElement("li");
                newShift.innerText = "Date: " + s.date.slice(0,-9) + " Time: " + s.startTime + " - " + s.endTime;
                child.appendChild(newShift);
            }
            //create CRUD actions for each epmloyee
            const Actions = newRow.insertCell();
            //EDIT
            const editLink = document.createElement("a");
            editLink.innerText = "Edit";
            editLink.setAttribute("href", 'editEmployee.html?userId='+userId+'&'+'empId='+d.ID);
            Actions.appendChild(editLink);
            //DELETE
            const deleteLink = document.createElement("a");
            deleteLink.innerText = "Delete";
            deleteLink.setAttribute("onclick", 'deleteEmployee('+ d.ID +')');
            deleteLink.setAttribute("href", "#");
            Actions.appendChild(deleteLink);
        }
    }else {
        alert("too many actions")
        window.location.href = "../../login.html";
    }
}

//delete employee from all
async function deleteEmployee(id) {
    const fetchParams = {
        method : 'delete',
        headers : {"Content-type" : "application/json"}
        }
        const res = await fetch("https://localhost:44351/api/Employees/"+id+"/"+userId, fetchParams);
        const data = await res.json(); 
        alert(data);
        location.reload();
}

//load edit page
async function loadEditEmployee() {
    //get all departments to select
    const res = await fetch("https://localhost:44351/api/Departments");
    const data = await res.json();
    //add departments to select
    for(let d of data) {
        const newOpt = document.createElement("option");
        newOpt.innerText = d.name;
        newOpt.setAttribute("value", d.ID);
        document.getElementById("departmentInp").appendChild(newOpt);
    }
}

//edit certain employee
async function editEmployee() {
    const currentEmp = {
        fullname : document.getElementById("nameInp").value,
        startWorkYear : document.getElementById("startWYInp").value,
        departmentID : document.getElementById("departmentInp").value
    }
    
    const fetchParams = {
        method : 'put',
        body : JSON.stringify(currentEmp),
        headers : {"Content-type" : "application/json"}
    }

    const res = await fetch("https://localhost:44351/api/Employees/"+empId+"/"+userId, fetchParams);
    const data = await res.json();
    alert(data);
    window.location.href = "employeesMain.html?userId="+userId;
}