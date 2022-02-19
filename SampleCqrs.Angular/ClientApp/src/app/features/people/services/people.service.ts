import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { Person } from '../models/person';

@Injectable({
  providedIn: 'root'
})
export class PeopleService extends BaseService {
  //
  private controllerName: string = 'People';
  //
  public count() {
    var action = `${this.controllerName}/Count`;
    return this.Get(action);
  }

  public get(currentPage: number, pageSize: number) {
    var action = `${this.controllerName}/GetPeople`;
    var paramters = `${currentPage * pageSize},${pageSize}`;
    return this.Get(action, paramters)
  }
  //
  public create(person: Person) {
    var action = `${this.controllerName}/Create`;
    return this.Post(action, person);
  }
  //
  public update(person: Person) {
    var action = `${this.controllerName}/Update`;
    return this.Put(action, person);
  }
  //
  public delete(id: string) {
    var action = `${this.controllerName}/Delete`;
    return this.Delete(action, id);
  }
  //
}
