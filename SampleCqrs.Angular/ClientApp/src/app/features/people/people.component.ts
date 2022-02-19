import { Component, OnInit, ViewChild } from '@angular/core';
import { Person } from './models/person';
import { PeopleService } from './services/people.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CreatePersonComponent } from './create-person/create-person.component';
import { UpdatePersonComponent } from './update-person/update-person.component';
import { DeletePersonComponent } from './delete-person/delete-person.component';

@Component({
  selector: 'app-people',
  templateUrl: './people.component.html',
  styleUrls: ['./people.component.css']
})
export class PeopleComponent implements OnInit {

  _people: MatTableDataSource<Person> = new MatTableDataSource<Person>([]);
  _currentPage: number = 0;
  _pageSize: number = 10;
  _totalCount: number = 0;
  _pageSizeOptions = [5, 10, 25, 100];
  // Angular maetiral sofigurations
  displayedColumns: string[] = ['firstName', 'lastName', 'lastModified', 'actions'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private _peopleService: PeopleService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadTotalCount();
  }

  loadTotalCount() {
    this._peopleService.count().subscribe((totalCount: number) => {
      this._totalCount = totalCount;
      if (totalCount > 0) {
        this.loadPeople();
      }
    });
  }

  loadPeople() {
    this._peopleService.get(this._currentPage, this._pageSize).subscribe((people) => {
      this._people = new MatTableDataSource<Person>(people);
    });
  }

  onPageChaned(event: PageEvent) {
    this._pageSize = event.pageSize;
    this._currentPage = event.pageIndex;
    this.loadPeople();
  }

  setPageSizeOptions(setPageSizeOptionsInput: string) {
    if (setPageSizeOptionsInput) {
      this._pageSizeOptions = setPageSizeOptionsInput.split(',').map(str => +str);
    }
  }

  announceSortChange(sortState: Sort) {
    // This example uses English messages. If your application supports
    // multiple language, you would internationalize these strings.
    // Furthermore, you can customize the message to add additional
    // details about the values being sorted.
    console.log("sort", sortState)
    // if (sortState.direction) {
    //   this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    // } else {
    //   this._liveAnnouncer.announce('Sorting cleared');
    // }
  }

  addPersonHandler() {
    const dialogRef = this.dialog.open(CreatePersonComponent, {
      data: new Person()
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result == 1) {
        //this.refresh();
      }
    });

  }

  updatePersonHandler(person: Person) {
    // this.id = request.id;
    // this.index = i;
    const dialogRef = this.dialog.open(UpdatePersonComponent, {
      // height: '400px',
      // width: '600px',
      data: person
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log("result", result)
      if (result == 1) {
        this.loadTotalCount();
      }
    });
  }

  deletePersonHandler(person: Person) {
    // this.id = request.id;
    // this.index = i;
    const dialogRef = this.dialog.open(DeletePersonComponent, {
      data: person
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.loadTotalCount();
      }
    });
  }

  createPersonHandler() {
    //
    const dialogRef = this.dialog.open(CreatePersonComponent, {
      // height: '400px',
      // width: '600px',
    });
    //
    dialogRef.afterClosed().subscribe(result => {
      if (result == 1) {
        this.loadTotalCount();
      }
    });
  }


}
