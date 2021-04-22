
@extends('layouts.app')

@section('content')

<div class="row">
 <div class="col-sm-8 offset-sm-2">
    <h3 class="display-4 text-center mb-5">Add a User</h3>

    <form method="post" action="{{route('admin.users.store')}}" enctype="multipart/form-data">
      @csrf
      
          <div class="form-group">    
              <label for="name">Name:</label>
              <input type="text" class="form-control" name="name"/>
          </div>

          <div class="form-group">
              <label for="email">Email:</label>
              <input type="text" class="form-control" name="email"/>
          </div>
          
          <div class="form-group">
              <label for="password">Password:</label>
              <input type="password" class="form-control" name="password"/>
          </div>

          <button type="submit" class="btn btn-success">Post</button>
          </form>
  </div>


</div> 

@endsection