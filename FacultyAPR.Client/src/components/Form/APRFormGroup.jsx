import React from 'react'
import Form from 'react-bootstrap/Form'
import Container from 'react-bootstrap/Container'
import APRFormSection from './APRFormSection'
import {FacultyReviewComment} from './FacultyReviewComment'

const APRFormGroup = ({state, group, onSectionChange, onReviewSectionChange, user}) => {
    return (
        <Form.Group>
            <Form.Label>{group.title}</Form.Label>
            <Form.Text>{group.description}</Form.Text>
            <Container>
            {group.sections.map((section, index) => {
                return <APRFormSection
                key = {index} 
                section={section}
                state={state}
                user={user}
                onUpdate={onSectionChange}/>
            })}
            <FacultyReviewComment state={state} user={user} content={group.comments} groupId={group.groupId} onUpdate={onReviewSectionChange} />
            </Container>
        </Form.Group>
    )
}

export {APRFormGroup};