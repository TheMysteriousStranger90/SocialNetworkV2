import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { preventUnsavedChangesGuard } from './prevent-unsaved-changes.guard';

describe('preventUnsavedChangesGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => preventUnsavedChangesGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
