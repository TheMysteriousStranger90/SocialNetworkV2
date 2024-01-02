import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LikesComponent } from './likes.component';

describe('LikesComponent', () => {
  let component: LikesComponent;
  let fixture: ComponentFixture<LikesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LikesComponent]
    });
    fixture = TestBed.createComponent(LikesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
