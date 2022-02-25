import {CREATE_FORM, UPDATE_REVIEW_SECTION, UPDATE_SECTION, UPDATE_STATE, UPDATE_REVIEW_COMMENT, UPDATE_FACULTY_COMMENT} from './formActionTypes'

import Form from '../../models/BusinessObjects/Form/Form'
import FormState from '../../models/BusinessObjects/Form/FormState';


export const createForm = (form: Form) => ({
    type: CREATE_FORM,
    payload: {
        form: form
    }
});

export const updateSection = (sectionId: string, groupId: string, content: string) => ({
    type: UPDATE_SECTION,
    payload: {
        sectionId: sectionId,
        groupId: groupId,
        content: content
    }
});

export const updateReviewSection = (groupId: string, content: string) => ({
    type: UPDATE_REVIEW_SECTION,
    payload: {
        groupId: groupId,
        content: content
    }
});

export const update_state = (state: FormState) => ({
    type: UPDATE_STATE,
    payload: {
        state: state
    }
});

export const updateFacultyComment = (facultyComment: string) => ({
    type: UPDATE_FACULTY_COMMENT,
    payload: {
        facultyComment: facultyComment
    }
});
export const updateReviewComment = (review: string) => ({
    type: UPDATE_REVIEW_COMMENT,
    payload: {
        review: review
    }
});