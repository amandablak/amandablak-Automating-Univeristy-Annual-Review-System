import React from 'react'
import Form from 'react-bootstrap/Form'

const FormTextInput = ({onChange, content, isReadonly}) => {
    if(isReadonly)
    {
        return (
            <Form.Control readOnly defaultValue={content}/>
        )
    }
    else
    {
        return (
            <Form.Control as="textarea" value={content} rows={3} onChange={onChange}/>
        )
    }
}

export {FormTextInput};