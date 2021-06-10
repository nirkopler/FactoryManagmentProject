//JS Functions Page :)

window.onload = () => {
    const tablePlace = document.getElementById("tablePlace");
    const searchInp = document.getElementById("searchInp");
    const searchByInp = document.getElementById("searchByInp");
}

//create navbar on each page
function loadNav() {
    //add container to top of BODY
    const navbarConatiner = document.createElement("div");
    navbarConatiner.setAttribute("class", "container");
    document.body.insertBefore(navbarConatiner, document.body.firstChild);
    
    //create navbar
    const navbar = document.createElement("div");
    navbar.style.backgroundColor = "red";
    navbar.setAttribute("class", "row is-center");
    
    //create logout button
    const logoutBtn = document.createElement("button");
    logoutBtn.setAttribute("onclick", 'logoutBtn()');
    logoutBtn.setAttribute("class", "col-2 is-marginless")
    logoutBtn.innerHTML = "Logout";

    //create username
    const navname = document.createElement("span");
    navname.setAttribute("class", "col-2 is-marginless")
    navname.style.color = "white";
    navname.innerHTML = sessionStorage.getItem('username');
    
    //reset file path
    let file = window.location.pathname;
    file = file.split("/").pop();
    let url;
    if(file === "index.html"){url = "index.html?userId=" + userId}
    else{url = "../../index.html?userId=" + userId}
    
    //create logo in center
    const navLogo = document.createElement("a");
    navLogo.setAttribute("class", "col-6 text-center is-marginless");
    navLogo.setAttribute("href", url)
    navLogo.style.fontSize = "30px";
    navLogo.style.color = "white";
    navLogo.innerHTML = "FAMA";
    
    navbar.appendChild(navname);
    navbar.appendChild(navLogo);
    navbar.appendChild(logoutBtn);
    navbarConatiner.appendChild(navbar);
}

//add CSS to all pages 
chota_css = '<link rel="stylesheet" href="https://unpkg.com/chota@latest">';
document.head.innerHTML += chota_css;

//Get urlParams On Each PageS
const urlParams = new URLSearchParams(window.location.search);
const userId = urlParams.get('userId');
const empId = urlParams.get('empId');
const depId = urlParams.get('depId');
const inp = urlParams.get('inp');
const by = urlParams.get('by');
console.log("userId: " + userId);
console.log("empId: " + empId);
console.log("depId: " + depId);
console.log("inp: " + inp);
console.log("by: " + by);

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
        const username = sessionStorage.setItem('username', data.fullname);
        } else {alert("too many actions per day")}
    } else {alert("worng user / pass")};
}

function logoutBtn() {
    let file = window.location.pathname;
    file = file.split("/").pop();
    let url;
    console.log(file)
    if(file === "index.html"){url = "login.html"}
    else{url = "../../login.html"}
    window.location.href = url;
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
            newRow.setAttribute("class", "bd-light")
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
            editLink.setAttribute("class", "button col");
            editLink.setAttribute("href", 'editEmployee.html?userId='+userId+'&'+'empId='+d.ID);
            Actions.appendChild(editLink);
            //DELETE
            const deleteLink = document.createElement("a");
            deleteLink.innerText = "Delete";
            deleteLink.setAttribute("class", "button error col");
            deleteLink.setAttribute("onclick", 'deleteEmployee('+ d.ID +')');
            deleteLink.setAttribute("href", "#");
            Actions.appendChild(deleteLink);
            //ADD SHIFT TO EMPLOYEE
            const addShiftLink = document.createElement("a");
            addShiftLink.innerText = "Add Shift To Emp";
            addShiftLink.setAttribute("class", "button primary col");
            addShiftLink.setAttribute("href", 'addShiftEmployee.html?userId='+userId+'&'+'empId='+d.ID);
            Actions.appendChild(addShiftLink);
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

//load shift data to add shift to employee
async function loadEditShiftEmployee() {
    //get all shifts to select
    const res = await fetch("https://localhost:44351/api/Shifts");
    const data = await res.json();
    //add shifts to select
    for(let d of data) {
        const newOpt = document.createElement("option");
        newOpt.innerText = "Date: " + d.date.slice(0,-9) + " Time: " + d.startTime + " - " + d.endTime;
        newOpt.setAttribute("value", d.ID);
        document.getElementById("shiftsInp").appendChild(newOpt);
    }
}

//add shift to employee
async function addShiftToEmployee() {
    const newShiftToEmp = {
        employeeID : empId,
        shiftID : document.getElementById("shiftsInp").value
    }

    const fetchParams = {
        method : 'post',
        body : JSON.stringify(newShiftToEmp),
        headers : {"Content-type" : "application/json"}
    }

    const res = await fetch("https://localhost:44351/api/Employees/"+userId, fetchParams);
    const data = await res.json();
    alert(data);
    window.location.href = "employeesMain.html?userId="+userId;
}

//load search employee
async function loadSearchEmployee() {
    //get search result
    const res = await fetch("https://localhost:44351/api/Employees/search/"+inp+'/'+by+'/'+userId);
    const data = await res.json();
    console.log(data);

    //create table with results
    if(data != null){
        if(data.length != 0){
            //create row for each employee
            for (let d of data) {
                const newRow = tablePlace.insertRow();
                newRow.setAttribute("class", "bd-light")
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
            }
        } else {
            //if theres no result
            tablePlace.remove();
            const err = document.createElement("h3");
            err.innerText = "No Result";
            document.body.appendChild(err);
        }
    } else {
        alert("too many actions")
        window.location.href = "../../login.html";
    }
}

// ------------------------- DEPARTMENTS -------------------------

//get all data of departments and create action buttons
async function getAllDepartmentsData() {
    const res = await fetch("https://localhost:44351/api/Departments/all/" + userId);
    const data = await res.json();
    console.table(data);
    if(data != null) {
        //create row for each department
        for (let d of data){
            const newRow = tablePlace.insertRow();
            newRow.setAttribute("class", "bd-light")
            const name = newRow.insertCell().innerText = d.name;
            const managerID = newRow.insertCell().innerText = d.managerID;

            //create CRUD actions for each department
            const Actions = newRow.insertCell();
            //EDIT
            const editLink = document.createElement("a");
            editLink.innerText = "Edit";
            editLink.setAttribute("class", "button col");
            editLink.setAttribute("href", 'editDepartment.html?userId='+userId+'&'+'depId='+d.ID);
            Actions.appendChild(editLink);
            //DELETE - if there is NO Epmloyees in certain Department
            if(!d.areAnyEmployees){
                const deleteLink = document.createElement("a");
                deleteLink.innerText = "Delete";
                deleteLink.setAttribute("class", "button col error");
                deleteLink.setAttribute("onclick", 'deleteDepartment('+ d.ID +')');
                deleteLink.setAttribute("href", "#");
                Actions.appendChild(deleteLink);
            }
        }
    } else {
        alert("too many actions")
        window.location.href = "../../login.html";
    }
}

//delete department
async function deleteDepartment(id) {
    const fetchParams = {
        method : 'delete',
        headers : {"Content-type" : "application/json"}
        }
        const res = await fetch("https://localhost:44351/api/Departments/"+id+"/"+userId, fetchParams);
        const data = await res.json(); 
        alert(data);
        location.reload();
}

//edit certain Department
async function editDepartment() {
    const currentDep = {
        name : document.getElementById("nameInp").value,
        managerID : document.getElementById("managerIDInp").value,
    }
    
    const fetchParams = {
        method : 'put',
        body : JSON.stringify(currentDep),
        headers : {"Content-type" : "application/json"}
    }

    const res = await fetch("https://localhost:44351/api/Departments/"+depId+"/"+userId, fetchParams);
    const data = await res.json();
    alert(data);
    window.location.href = "departmentsMain.html?userId="+userId;
}

//add department
async function addDepartment() {
    const currentDep = {
        name : document.getElementById("nameInp").value,
        managerID : document.getElementById("managerIDInp").value,
    }
    
    const fetchParams = {
        method : 'post',
        body : JSON.stringify(currentDep),
        headers : {"Content-type" : "application/json"}
    }

    const res = await fetch("https://localhost:44351/api/Departments/"+userId, fetchParams);
    const data = await res.json();
    alert(data);
    window.location.href = "departmentsMain.html?userId="+userId;
}

// ------------------------- SHIFTS -------------------------
//get all data of shifts and create action buttons
async function getAllShiftsData() {
    const res = await fetch("https://localhost:44351/api/Shifts/all/" + userId);
    const data = await res.json();
    console.table(data);
    if(data != null){
        //create row for each shifts
        for (let d of data) {
            const newRow = tablePlace.insertRow();
            newRow.setAttribute("class", "bd-light")
            const date = newRow.insertCell().innerText = d.date.slice(0,-9);
            const time = newRow.insertCell().innerText = d.startTime + " - " + d.endTime;
            const employees = newRow.insertCell();
            const child = document.createElement("ul");
            employees.appendChild(child);
            //create employee for each shift
            for (let e of d.empList) {
                //add employee fullname
                const newEmployee = document.createElement("li");
                newEmployee.innerText = e.fullname;
                //add edit employee link
                const editLink = document.createElement("a");
                editLink.innerText = "Edit Employee";
                editLink.setAttribute("class", "col-5 button")
                editLink.setAttribute("href", '../employees/editEmployee.html?userId='+userId+'&'+'empId='+e.ID);
                newEmployee.appendChild(editLink);
                child.appendChild(newEmployee);
            }
        }
    }else {
        alert("too many actions")
        window.location.href = "../../login.html";
    }
}

//add shift
async function addShift() {
    const newShift = {
        date : document.getElementById("dateInp").value,
        startTime : document.getElementById("startTimeInp").value,
        endTime : document.getElementById("endTimeInp").value
    }

    const fetchParams = {
        method : "post",
        body : JSON.stringify(newShift),
        headers : {"Content-type" : "application/json"}
    }

    const res = await fetch("https://localhost:44351/api/Shifts/"+userId, fetchParams);
    const data = await res.json();
    alert(data);
    window.location.href = "shiftsMain.html?userId="+userId;
}