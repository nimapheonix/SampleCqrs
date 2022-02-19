import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Person } from '../models/person';
import { PeopleService } from '../services/people.service';

@Component({
  selector: 'app-update-person',
  templateUrl: './update-person.component.html',
  styleUrls: ['./update-person.component.css']
})
export class UpdatePersonComponent implements OnInit {

  _person!: Person;

  personForm = new FormGroup({
    Id:new FormControl('',[Validators.required]),
    RowVersion:new FormControl('',[Validators.required]),
    FirstName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(64)
    ]),
    LastName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(64)
    ])
  });

  constructor(private dialogRef: MatDialogRef<UpdatePersonComponent>,
    @Inject(MAT_DIALOG_DATA) private person: Person,
    private peopleService: PeopleService) {
    if (person == null || person == undefined) { return; }
    this._person = person;
    this.initForm(person);
  }

  ngOnInit(): void {
  }

  initForm(person: Person) {
    this.personForm.controls["Id"].setValue(person.id);
    this.personForm.controls["RowVersion"].setValue(person.rowVersion);
    this.personForm.controls["FirstName"].setValue(person.firstName);
    this.personForm.controls["LastName"].setValue(person.lastName);
  }

  onCancel() { 
    this.dialogRef.close(-1);
  }

  onSave() {
    this.peopleService.update(this.personForm.value).subscribe((resp)=>{
      this.dialogRef.close(1);
    });
  }



}
