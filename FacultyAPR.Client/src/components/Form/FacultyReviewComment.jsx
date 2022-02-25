import React from 'react'
import Form from 'react-bootstrap/Form'
import {FormTextInput} from './FormTextInput'

const FacultyReviewComment = ({onUpdate, groupId, content, state, user}) => {
    const isReadonly = IsReadonly(state, user)
    const handleContentChange = (id, event) => {
        onUpdate(id, event.target.value);
    }
    return (
        <div>
            <Form.Label>Faculty Comments</Form.Label>
            <FormTextInput content={content} onChange={event => handleContentChange(groupId, event)} isReadonly={isReadonly}/>
        </div>
    )
}

const IsReadonly = (state, user) => {
    return state != 'Review' 
        || (user.userType != "FacultyChair" 
        && user.userType != "Dean")
}

export {FacultyReviewComment}