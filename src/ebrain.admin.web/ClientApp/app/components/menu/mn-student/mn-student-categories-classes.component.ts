import { Component, OnInit, OnDestroy, TemplateRef, ViewChild } from '@angular/core';
import { fadeInOut } from '../../../services/animations';

@Component({
    selector: 'mn_student_categories_classes',
    templateUrl: './mn-student-categories-classes.component.html',
    styleUrls: ['./mn-student-categories-classes.component.css'],
    animations: [fadeInOut]
})

export class mn_student_categories_classesComponent implements OnInit, OnDestroy {
    ngOnDestroy() { }
    ngOnInit() { }
}
    