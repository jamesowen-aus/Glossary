import React, { Component } from 'react';
import { AddEditTerm } from './AddEditTerm';

export class Glossary extends Component {
    static displayName = Glossary.name;

      constructor(props) {
          super(props);
          this.state = {
              glossaryItems: [],
              loading: true,
              editableItem: { id: 0, term: '', definition: '' },
              formVisible: false,
              error: { title: '', errors:[]}
          };

          this.populateGlossaryData = this.populateGlossaryData.bind(this);
          this.saveItem = this.saveItem.bind(this);
          this.addItemToState = this.addItemToState.bind(this);
          this.removeItemFromState = this.removeItemFromState.bind(this);
          this.showForm = this.showForm.bind(this);
          this.saveItem = this.saveItem.bind(this);
      }

     componentDidMount() {
       this.populateGlossaryData();
     }

    async populateGlossaryData() {
        this.setState({ loading: true });
        const response = await fetch('glossaryitems');
        const data = await response.json();
        data.sort((a, b) => a.term.localeCompare(b.term));
        this.setState({ glossaryItems: data, loading: false });
    }

    addItemToState(itemArray, newItem) {
        itemArray.push({ ...newItem });
        itemArray.sort((a, b) => a.term.localeCompare(b.term));
        this.setState({ glossaryItems: itemArray });
    }

    updateItemInState(itemArray, updatedItem) {
        const item = itemArray.find(
            i => i.id === updatedItem.id
        );
        item.term = updatedItem.term;
        item.definition = updatedItem.definition;

        itemArray.sort((a, b) => a.term.localeCompare(b.term));
        this.setState({ glossaryItems: itemArray });
    }

    removeItemFromState(itemArray, itemId) {
        this.setState({ glossaryItems: itemArray.filter((item) => item.id !== itemId) });
    }

    async saveItem(item) {
        if (item.id === 0) {
            this.sendSave(item);
        } else {
            this.sendUpdate(item);
        }
    }

    async sendSave(newItem) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newItem)
        };

        let response;
        let data;

        try {
            response = await fetch('glossaryitems', requestOptions);
            data = await response.json();
        } catch (error) {
            console.log('Error connecting to server', error);
        }

        if (response?.ok) {
            //if POST is successful then add the item to the state
            //No need to hit api again to reload all items
            this.addItemToState([...this.state.glossaryItems], data);
            this.showForm(false);
        } else {
            this.setState({ error: {title: data.title, errors: data.errors} });
            console.log(`HTTP Response Code: ${response?.status}`)
        }

    }

  

    async sendUpdate(updateItem) {
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updateItem)
        };

        let response;
        let data;
        try {
            response = await fetch('glossaryitems/' + updateItem.id, requestOptions);
            data = await response.json();
        } catch (error) {
            console.log('Error connecting to server', error);
        }

        if (response?.ok) {
            //if PUT is successful then add the item to the state
            //No need to hit api again to reload all items
            this.updateItemInState([...this.state.glossaryItems], updateItem);
            this.showForm(false);
        } else {
            this.setState({ error: { title: data.title, errors: data.errors } });
            console.log(`HTTP Response Code: ${response?.status}`)
        }
    }

    async deleteItem(itemId) {
        const requestOptions = {
            method: 'DELETE'
        };

        let response;;

        try {
            response =  await fetch('glossaryitems/' + itemId, requestOptions);
        } catch (error) {
            console.log('Error connecting to server', error);
        }

        if (response?.ok) {
        //No need to call the glossaryItems array and reload the collection.
        //If delete request is succussful remove item from the state.
            this.removeItemFromState([...this.state.glossaryItems], itemId);
        } else {
            alert('Record could not be deleted.');
            console.log(`HTTP Response Code: ${response?.status}`)
        }
    }

    showForm(visible) {
        this.setState({ error: { title: '', errors: [] } });
        this.setState({ formVisible: visible });
    }

    showFormForAdd() {
        this.setState({ editableItem: { id: 0, term: '', definition:'' } });
        this.showForm(true);
    }

    showFormForEdit(item) {
        this.setState({ editableItem: item });
        this.showForm(true);
    }

    renderGlossaryItemsTable(glossaryItems) {
        return (
            <table className="table table-striped glossary-table" aria-labelledby="tableLabel">
           
                <thead>
                    <tr>
                        <th>Term</th>
                        <th>Definition</th>
                        <th></th>
                    </tr>
            </thead>
            <tbody>
                {glossaryItems.map(item =>
                     <tr key={item.id}>
                        <td className="col-term">{item.term}</td>
                        <td className="col-definition">{item.definition}</td>
                        <td>
                            <button className="btn btn-link" disabled={this.state.formVisible} onClick={() => this.showFormForEdit(item)}>Edit</button>
                            <button className="btn btn-link" disabled={this.state.formVisible} onClick={() => this.deleteItem(item.id)}>Delete</button>
                        </td>
                    </tr>
                    )}   
            </tbody>
          </table>
        );
     }

    render() {
        
        let contents = this.state.loading
          ? <p><em>Loading...</em></p>
            : this.renderGlossaryItemsTable(this.state.glossaryItems);

        return (
            <div>
                <div className="row">
                    <div className="col-lg-11 formSection">
                        {this.state.formVisible &&
                            <AddEditTerm
                                onSubmit={this.saveItem}
                                onCancel={this.showForm}
                                formError={this.state.error}
                                termId={this.state.editableItem.id}
                                term={this.state.editableItem.term}
                                definition={this.state.editableItem.definition}
                            />}
                    </div>
                </div>
                <div className="row buttonSection">
                    <div className="col-lg-8 pull-left">
                        {!this.state.formVisible && <button className="btn btn-primary btn-sm"
                            onClick={() => this.showFormForAdd()}>Add Term</button>}
                    </div>
                    <div className="col-lg-3 pull-right">
                       
                    </div>
                    <div className="col-lg-1 pull-right">
                        <button className="btn btn-primary btn-sm"
                            onClick={e => this.populateGlossaryData().then(() => e.target.blur())}>Refresh</button>
                    </div>
                </div>
               
                {contents}
            </div>
        );
    }

    
}
