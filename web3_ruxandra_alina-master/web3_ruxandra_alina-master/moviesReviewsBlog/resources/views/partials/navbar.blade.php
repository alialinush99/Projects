<nav class="navbar navbar-expand-md navbar-light bg-white shadow-sm">
     <div class="container">
         <a class="navbar-brand" href="{{ url('/') }}">
             {{ config('app.name', 'Laravel') }}
         </a>
         <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="{{ __('Toggle navigation') }}">
             <span class="navbar-toggler-icon"></span>
         </button>

         <div class="collapse navbar-collapse" id="navbarSupportedContent">
             <!-- Left Side Of Navbar -->
             <ul class="navbar-nav mr-auto">
                 <li class="nav-item">
                     <a class="nav-link" href="{{ url('/')}}">Home</a>
                 </li>
                 <li class="nav-item">
                     <a class="nav-link" href="{{ url('/movies/')}}">Movies</a>
                 </li>
                 @can('admin')
                    <li class="nav-item">
                        <a class="nav-link" href="{{ url('admin/users')}}">Administration Panel</a>
                    </li>
                 @endcan
                 <li class="nav-item">
                     <a class="nav-link" href="{{ url('/posts')}}">Reviews</a>
                 </li>
                 <li class="nav-item">
                    <a class="nav-link" href="{{ url('/home')}}">My Reviews</a>
                </li>
             </ul>
             <ul class="nav navbar-nav">
                 <!-- <li><a href="/">Home</a></li> -->
                 <!-- <li><a href="/movies">Movies</a></li> -->
             </ul>


             <!-- Right Side Of Navbar -->

             <ul class="navbar-nav ml-auto">
                 <!-- Authentication Links -->
                 @guest
                 <li class="nav-item">
                     <a class="nav-link" href="{{ route('login') }}">{{ __('Login') }}</a>
                 </li>
                 @if (Route::has('register'))
                 <li class="nav-item">
                     <a class="nav-link" href="{{ route('register') }}">{{ __('Register') }}</a>
                 </li>
                 @endif
                 @else
                 <!-- <ul class="navbar-nav navbar-right"> -->
                    <li class="nav-item">
                        <a class="nav-link" href="{{ url('/posts/create')}}">Create Review</a>
                    </li>
                     <li>
                         <a class="nav-link" href="{{ url('/users/' . Auth::user()->id )}}" role="button">
                             
                             {{ Auth::user()->name }} <span class="caret"></span>
                         </a>
                     </li>




                     <li>
                <a class="nav-link" href="{{url('/users/' . Auth::user()->id)}}" role="button">
                    <img style="border-radius:50%" src="{{asset(auth()->user()->getAvatar())}}" alt="Profile Image" width="25" height="25">
                     <span class="caret"></span>
                </a>
            </li>


                     <li class="nav-item">
                         <a href="{{ route('logout') }}" class="nav-link" onclick="event.preventDefault();
                                                     document.getElementById('logout-form').submit();">
                             {{ __('Logout') }}
                         </a>

                         <form id="logout-form" action="{{ route('logout') }}" method="POST" style="display: none;">
                             @csrf
                         </form>
                     </li>
                 <!-- </ul> -->
                 @endguest
             </ul>

         </div>
     </div>
 </nav>