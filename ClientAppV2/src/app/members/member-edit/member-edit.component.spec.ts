import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberEditComponent } from './member-edit.component';

describe('MemberEditComponent', () => {
  let component: MemberEditComponent;
  let fixture: ComponentFixture<MemberEditComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberEditComponent]
    });
    fixture = TestBed.createComponent(MemberEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
