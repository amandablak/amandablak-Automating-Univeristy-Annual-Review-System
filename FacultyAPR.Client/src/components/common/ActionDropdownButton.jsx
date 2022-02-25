import React from 'react'
import Dropdown from 'react-bootstrap/Dropdown'

const ActionDropdownButton = ({title, actions}) => {
    return(
        <Dropdown>
            <Dropdown.Toggle variant="info" id="dropdown-basic">
                {title}
            </Dropdown.Toggle>
            <Dropdown.Menu>
                {actions.map((action, index) => {
                    return <Dropdown.Item key={index} onClick={() => action.onClick(action.Name)}>{action.Name}</Dropdown.Item>
                })}
            </Dropdown.Menu>
        </Dropdown>
    )
}

export {ActionDropdownButton}