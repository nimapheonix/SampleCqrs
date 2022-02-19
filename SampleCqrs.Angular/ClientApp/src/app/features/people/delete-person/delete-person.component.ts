import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Person } from '../models/person';
import { PeopleService } from '../services/people.service';

@Component({
  selector: 'app-delete-person',
  templateUrl: './delete-person.component.html',
  styleUrls: ['./delete-person.component.css']
})
export class DeletePersonComponent implements OnInit {

  _person!: Person;

  constructor(private dialogRef: MatDialogRef<DeletePersonComponent>,
    @Inject(MAT_DIALOG_DATA) private person: any,
    private peopleService: PeopleService) {
    if (person == null || person == undefined) { return; }
    this._person = person;
  }

  ngOnInit(): void {
  }

  onNoClick(): void {
    this.dialogRef.close(-1);
  }

  confirmDelete(): void {
    if (this._person == null || this._person == undefined) { return; }
    this.peopleService.delete(this._person.id).subscribe((resp)=>{
      this.dialogRef.close(1);
    });
  }

}
