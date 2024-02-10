* create an employee from api
    - [x]POST employees/ [Manager]
    - [x]GET employees/
* login/logout
    - [x]POST sessions/
    - [x]DELETE sessions/
* apply for leave
    - [x]POST leaves/ (check if this is working)
* approve leaves
    - [x]PUT leaves/{id}
    - [x][Managers -> employees leaves]
* see all the details of an employee
    - GET employees/{id} [employee having that id, manager of the employee] (Get the leave details too)
* see all reportees
    - GET employees/{managerid}/reportees

---

- A manager should be able to add employees.
- An employee should be able to apply for leaves.
- A manager should be able to approve leaves.
- An employee should be able to view his details alone, including approved/pending leaves.
- A manager should be able to see all his reportees

**Login/Logout API's should be build.
**Use Cookie based Auth.
**Need not use any Databases for this, try to keep singleton class instead.
