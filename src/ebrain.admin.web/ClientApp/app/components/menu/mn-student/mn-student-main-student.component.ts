import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_student_main_student',
    templateUrl: './mn-student-main-student.component.html',
    styleUrls: ['./mn-student-main-student.component.css'],
    animations: [fadeInOut]
})

export class mn_student_main_studentComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
