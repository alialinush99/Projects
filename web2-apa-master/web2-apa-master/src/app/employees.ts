export class Employee
{
  id: number;
  department_id: number;
  first_name: string;
  last_name:string;
  birth_date:string;


  constructor(id:number,department_id:number,first_name:string,last_name:string,birth_date:string)
  {
    this.id=id;
    this.department_id=department_id;
    this.first_name=first_name;
    this.last_name=last_name;
    this.birth_date=birth_date;
  }
}
