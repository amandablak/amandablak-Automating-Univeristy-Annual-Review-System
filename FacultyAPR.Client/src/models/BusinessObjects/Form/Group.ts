import Section from "./Section"

type Group = {
    groupId: string,
    title: string,
    description: string,
    comments: string,
    sections: Array<Section>,
    orderIndex: number
}

export default Group;