import GetFormStructureById from './Routing.ts'

import axios from 'axios';


jest.mock('axios');

describe('GetFormStructureById', () => {
  it('fetches form by id successfully', async () => {

    const data = {
      data: {
      hits: [
        {
          FormId: '1234567723', FacRank: "Lecturer", FormYear:"2020"
        }
      ],
    },
  };
  axios.get.mockImplementationOnce(() => Promise.resolve(data));
  });
 
  it('fails to fetch form by id', async () => {
    const errorMessage = 'Error';
    axios.get.mockImplementationOnce(() =>
    Promise.reject(new Error(errorMessage)),
    );
        
  });
 
  it('fails to fetch form by id', async () => {
 
  });
});



describe('GetFormStructureByClassification', () => {
  it('fetches form by classification successfully', async () => {
    
  });
 
  it('fails to fetch form by classification', async () => {
 
  });
});


describe('UpdateFormStructure', () => {
    it('updates the structure successfully', async () => {
      
    });
   
    it('fails to update the structure', async () => {
   
    });
  });

  describe('CreateFormStructure', () => {
    it('creates form successfully', async () => {
      
    });
   
    it('fails to create form', async () => {
   
    });
  });


  describe('DeleteFormStructure', () => {
    it('deletes form successfully', async () => {
      
    });
   
    it('fails to delete form', async () => {
   
    });
  });

  describe('GetFormContentById', () => {
    it('fetches form content by idsuccessfully', async () => {
      
    });
   
    it('fails to fetch form content by id', async () => {
   
    });
  });


