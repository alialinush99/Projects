export class Task {
    id: number;
    department_id: number;
    name: string;
    description: string
    employees: [];
    due_date: string;

    constructor(id: number, department_id: number, name: string, description: string, employees: [], due_date: string) {
        this.id = id;
        this.department_id = department_id;
        this.name = name;
        this.description = description;
        this.employees = employees;
        this.due_date = due_date;
    }
}

