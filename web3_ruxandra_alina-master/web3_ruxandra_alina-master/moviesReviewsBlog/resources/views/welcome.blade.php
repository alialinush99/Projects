 @extends('layouts.app')

@section('content')
        <!-- <div class="flex-center position-ref full-height">
            @if (Route::has('login'))
                <div class="top-right links">
                    @auth
                        <a href="{{ url('/home') }}">Home</a>
                    @else
                        <a href="{{ route('login') }}">Login</a>

                        @if (Route::has('register'))
                            <a href="{{ route('register') }}">Register</a>
                        @endif
                    @endauth
                </div>
            @endif -->

            <div class="content">
                <img src="/img/movie-reviews-design-2-1050x492.jpg" alt="movie reviews logo" height="400" width="600">
                <div class="title m-b-md">
                    Movie Reviews
                </div>


            </div>
        </div>
@endsection 
