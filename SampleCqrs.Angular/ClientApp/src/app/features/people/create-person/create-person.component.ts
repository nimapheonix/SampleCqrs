import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PeopleService } from '../services/people.service';

@Component({
  selector: 'app-create-person',
  templateUrl: './create-person.component.html',
  styleUrls: ['./create-person.component.css']
})
export class CreatePersonComponent implements OnInit {


  personForm = new FormGroup({
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

  constructor(public dialogRef: MatDialogRef<CreatePersonComponent>,
    private peopleService: PeopleService) { }

  ngOnInit(): void {
  }

  onCancel() {
    this.dialogRef.close(-1);
  }

  onSave() {
    this.peopleService.create(this.personForm.value).subscribe((resp) => {
      console.log("success add")
      this.dialogRef.close(1);
    });
  }




}
