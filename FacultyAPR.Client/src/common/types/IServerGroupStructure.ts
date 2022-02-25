import SectionStructure from "../../models/BusinessObjects/Form/SectionStructure";

export default interface IServerGroupStructure {
    groupId: string,
    title: string,
    description: string,
    sections: Array<SectionStructure>,
    orderIndex: number
}
