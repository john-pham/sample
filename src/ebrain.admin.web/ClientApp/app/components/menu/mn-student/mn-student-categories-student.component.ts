import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_student_categories_student',
    templateUrl: './mn-student-categories-student.component.html',
    styleUrls: ['./mn-student-categories-student.component.css'],
    animations: [fadeInOut]
})

export class mn_student_categories_studentComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
